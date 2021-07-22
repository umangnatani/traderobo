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
    @Input() broker = "RH";

    Watchlists;
    watchlistId = 0;

    TDAccounts;
    TDAccountId = '';

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
        },

        {
            field: "GlobalQuote.lastPrice",
            headerName: "Price",
            cellStyle: { "font-weight": "bold" },
        },
        {
            field: "GlobalQuote.netPercentChangeInDouble",
            headerName: "% Change",
            cellStyle: { "font-weight": "bold" },
            valueFormatter: (params) => {
                return params.value.toFixed(2) + "%";
            },
        },
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

        this.rowClassRules = {
            "red-900-fg": "data.GlobalQuote.netPercentChangeInDouble < 0",
            "green-900-fg": "data.GlobalQuote.netPercentChangeInDouble > 0",
        };

        this.rowSelection = "multiple";
    }

    ngOnInit(): void {
        this.orderType = this.broker === "RH" ? "multi": "single";

        this.folioForm = this._formBuilder.group({
            Amount: [50],
            Side: ["buy"],
            Symbol: [""],
            Quantity: [10],
            Price: [null],
            Increment: [0.2],
            Total: [3],
        });

        this.form = this._formBuilder.group({
            Symbols: [],
        });

        
            
        this.loadTDAccounts();       

        this.loadWatchlist();

        this.loadWatchlistSymbols();
    }

    loadTDAccounts() {
        if(this.broker === "TD"){
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
        this.apiService
            .getPieDetail(this.watchlistId, "q")
            .subscribe((data) => {
                this.dataSource = data;
            });

    }

    get f() {
        return this.folioForm.controls;
    }

    onRowSelected(event) {
        
        this.CurSymbol = event.node.data.Symbol;
        this.f.Symbol.setValue(this.CurSymbol);
        this.f.Price.setValue((Math.round(event.node.data.GlobalQuote.lastPrice*100)/100).toFixed(2));
        this.f.Increment.setValue((Math.round(event.node.data.GlobalQuote.lastPrice*.007*100)/100).toFixed(2));
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
        //this.folioForm.controls.Side.setValue(val);
        this.broker = val;
        this.loadTDAccounts();       
    }

    placeOrder(): void {
        const payload = this.gridApi.getSelectedRows();

        if (!this.showAdvanced) {
            this.f.Total.setValue(1);
        }

        let data = this.folioForm.value;

        data.Symbols = payload;

        //console.log(data);

        this.Globals.IsBusy = true;
        this.apiService
            .placeComplexOrder(data, this.orderType, this.broker, this.TDAccountId)
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
