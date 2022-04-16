import { fuseAnimations } from './../../../../@fuse/animations/index';
import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import {
    FormBuilder,
    FormGroup,
    FormControl,
    Validators,
} from "@angular/forms";
import { Subject } from "rxjs";
import {
    ApiService,
    NotificationService,
    BaseComponent,
    Globals,
} from "app/my-app/_shared";

@Component({
    selector: "app-multi-order",
    templateUrl: "./multi-order.component.html",
    styleUrls: ["./multi-order.component.scss"],
})
export class MultiOrderComponent extends BaseComponent implements OnInit {
    @Input() broker;

    Watchlists;
    watchlistId = 2;

    TDAccounts;
    TDAccountId = "";

    form: FormGroup;
    isEdit = false;

    CurSymbol = "";

    pieValue = "";
    folioForm: FormGroup;

    dataSource;
    orderSide = "buy";
    orderType = "multi";

    showTrade = false;

    showAdvanced = false;

    defaultColDef;

    rowClassRules;

    rowSelection;

    private gridApi;

    columnDefs = [
        {
            field: "Symbol",
            cellStyle: { "font-weight": "bold", color: "blue" },
            cellRenderer: function(params) {
                return '<a href="https://www.cmlviz.com/stocks/' + params.value + '/pivot-points" target="_blank" rel="noopener">'+ params.value+'</a>'
              }
            
        },

        {
            field: "GlobalQuote.lastPrice",
            headerName: "Price",
            cellStyle: { "font-weight": "bold" },
            cellClassRules: {
                "red-900-fg": "data.GlobalQuote.isRed",
                "green-900-fg": "!data.GlobalQuote.isRed",
              },
        //     cellRenderer: function(params) {
        //         return '<span class="bg-pink-600 text-white rounded-full">50</span>'
        //    }
        },
        {
            field: "GlobalQuote.percentChange",
            headerName: "% Change",
            cellStyle: { "font-weight": "bold" },
            cellClassRules: {
                "red-900-fg": "data.GlobalQuote.isRed",
                "green-900-fg": "!data.GlobalQuote.isRed",
              },
            valueFormatter: (params) => {
                return params.value.toFixed(2) + "%";
            },
        },
        // {
        //     field: "ma50",
        //     headerName: "SMA 50",
        //     cellClass: "text-bold",
        //     cellClassRules: {
        //         'red-bg': 'x >= data.GlobalQuote.lastPrice',
        //         'green-bg': 'x <= data.GlobalQuote.lastPrice',
        //       },
        // },
        // {
        //     field: "ma200",
        //     headerName: "SMA 200",
        //     cellClass: "text-bold",
        //     cellClassRules: {
        //         'red-bg': 'x >= data.GlobalQuote.lastPrice',
        //         'green-bg': 'x <= data.GlobalQuote.lastPrice',
        //       },
        // },
        // {
        //     field: "ma5",
        //     headerName: "EMA 5",
        //     cellClass: "text-bold",
        //     cellClassRules: {
        //         'red-bg': 'x >= data.GlobalQuote.lastPrice',
        //         'green-bg': 'x <= data.GlobalQuote.lastPrice',
        //       },
        // },
        // {
        //     field: "ma10",
        //     headerName: "EMA 10",
        //     cellClass: "text-bold",
        //     cellClassRules: {
        //         'red-bg': 'x >= data.GlobalQuote.lastPrice',
        //         'green-bg': 'x <= data.GlobalQuote.lastPrice',
        //       },
        // },
        // {
        //     field: "ma21",
        //     headerName: "EMA 21",
        //     cellClass: "text-bold",
        //     cellClassRules: {
        //         'red-bg': 'x >= data.GlobalQuote.lastPrice',
        //         'green-bg': 'x <= data.GlobalQuote.lastPrice',
        //       },
        // },
    ];

    constructor(
        private _formBuilder: FormBuilder,
        public apiService: ApiService,
        private notificationService: NotificationService,
        private globals: Globals
    ) {
        super(apiService);

        this.defaultColDef = {
            sortable: true,
        };


        this.rowSelection = "multiple";
    }

    ngOnInit(): void {
        this.orderType = this.broker === "TD" ? "single" : "multi";

        this.folioForm = this._formBuilder.group({
            Amount: [50],
            Side: ["buy"],
            Symbol: [""],
            Quantity: [10],
            Price: [null],
            Increment: [0.2],
            Total: [3],
            Broker: [this.broker]
        });

        this.form = this._formBuilder.group({
            Symbols: [],
        });

        this.loadTDAccounts();

        this.loadWatchlist();

        this.loadWatchlistSymbols();
    }

    loadTDAccounts() {
        if (this.broker === "td") {
            this.apiService.getTDAccounts().subscribe((data) => {
                this.TDAccounts = data;
                this.TDAccountId = this.TDAccounts[0].AccountId;
            });
        }
    }

    selectAccount(obj): void {
        this.TDAccountId = obj.AccountId;
    }

    loadWatchlist() {
        this.apiService.getPies().subscribe((data) => {
            this.Watchlists = data;
        });
    }

    loadWatchlistSymbols() {
        if (this.watchlistId > 0) {
            this.apiService
                .getPieDetail(this.watchlistId, "q")
                .subscribe((data) => {
                    this.dataSource = data;
                });
        }
    }

    get f() {
        return this.folioForm.controls;
    }

    onRowSelected(event) {
        this.CurSymbol = event.node.data.Symbol;
        this.f.Symbol.setValue(this.CurSymbol);
        this.f.Price.setValue(
            (
                Math.round(event.node.data.GlobalQuote.lastPrice * 100) / 100
            ).toFixed(2)
        );
        this.f.Increment.setValue(
            (
                Math.round(
                    event.node.data.GlobalQuote.lastPrice * 0.007 * 100
                ) / 100
            ).toFixed(2)
        );
        this.showTrade = true;
    }

    public saveWatchlist() {
        // console.log(this.PieDetails);
        this.apiService
            .saveWatchListSymbols(this.watchlistId, this.form.value)
            .subscribe((data) => {
                // console.log(data);
                this.apiService.setMessage2(data);
                this.loadWatchlistSymbols();
                // this.activePie = this.form.value;
            });
    }

    selectWatchlist(watchlist): void {
        //console.log(watchlist.Id);
        this.watchlistId = watchlist.Id;
        this.isEdit = true;

        this.gridApi.deselectAll();

        this.loadWatchlistSymbols();
    }

    selectOrderSide(val: string): void {
        this.folioForm.controls.Side.setValue(val);
        this.orderSide = val;
    }

    selectOrderType(val: string): void {
        //this.folioForm.controls.Side.setValue(val);
        this.orderType = val;
    }

    selectBroker(val: string): void {
        this.broker = val;
        this.loadTDAccounts();
    }

    placeOrder(): void {
        const payload = this.gridApi.getSelectedRows();

        if (!this.showAdvanced) {
            this.f.Total.setValue(1);
        }

        this.f.Broker.setValue(this.broker);

        let data = this.folioForm.value;

        data.Symbols = payload;

        //console.log(data);

        this.Globals.IsBusy = true;
        this.apiService
            .placeComplexOrder(
                data,
                this.orderType,
                this.broker,
                this.TDAccountId
            )
            .subscribe((response) => {
                this.apiService.setMessage2(response);
                //this.dataSource = data.Object.Orders;
                this.Globals.IsBusy = false;
            });
    }

    checkAdvanced() {
        this.showAdvanced = !this.showAdvanced;
    }

    onGridReady(params) {
        this.gridApi = params.api;
        this.gridApi.sizeColumnsToFit();
    }

    ngOnDestroy(): void {}
}
