import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiService, NotificationService, BaseComponent, Globals } from 'app/my-app/_shared';

@Component({
    selector: 'app-folio',
    templateUrl: './folio.component.html',
    styleUrls: ['./folio.component.scss']
})
export class FolioComponent extends BaseComponent implements OnInit {
    @Input() broker;

    pieId = 2;
    folioForm: FormGroup;

    dataSource;
    orderSide = 'buy';

    PieDetails: ApiModel.PieDetail[];

    Total = 0;

    Pies;

    defaultColDef;

    rowClassRules;

    // columnDefs = [
    //     {field: 'Symbol' },
    //     {field: 'Weight' }
    // ];

    private gridApi;


    columnDefs = [
        { field: "Symbol",
        cellStyle: {'font-weight': 'bold', color:'blue'}, },

        {
            field: "Weight",
            cellStyle: {'font-weight': 'bold', color:'blue'},
        },
        {
            field: "GlobalQuote.lastPrice",
            headerName: "Last Price",
            cellClassRules: {
                "red-900-fg": "data.GlobalQuote.isRed",
                "green-900-fg": "!data.GlobalQuote.isRed",
              },
            cellStyle: {'font-weight': 'bold'},
        },
        {
            field: "GlobalQuote.percentChange",
            headerName: "% Change",
            cellClassRules: {
                "red-bg": "data.GlobalQuote.isRed",
                "green-bg": "!data.GlobalQuote.isRed",
              },
            cellStyle: {'font-weight': 'bold'},
            valueFormatter: (params) => {
                return params.value.toFixed(2) + '%';
            },
        },
    ];


    constructor(
        private _formBuilder: FormBuilder,
        protected apiService: ApiService
    ) {
        super(apiService);

        this.defaultColDef = {
            // width: 100,
            sortable: true,
        };

        this.rowClassRules = {
            'red-900-fg': 'data.GlobalQuote.netPercentChangeInDouble < 0',
            'green-900-fg': 'data.GlobalQuote.netPercentChangeInDouble > 0',
        };

    }


    ngOnInit(): void {
        this.folioForm = this._formBuilder.group({
            PieId: [this.pieId],
            Amount: [500, Validators.required],
            Side: ['buy'],
            PriceWeighted: [false],
            Broker: [this.broker]
        });


        // console.log(this.token.isAuthenticated);

        this.apiService.getPies().subscribe((data) => {
            this.Pies = data;
            this.loadPieSymbols();
        });

    }

    get f() {
        return this.folioForm.controls;
    }


    selectOrderSide(val: string): void {
        this.folioForm.controls.Side.setValue(val);
        this.orderSide = val;
    }

    selectBroker(val: string): void {
        //this.folioForm.controls.Side.setValue(val);
        this.broker = val;
    }


    public savePie() {
        // console.log(this.PieDetails);
        this.apiService.savePieDetail(this.PieDetails).subscribe((data) => {
            console.log(data);
            this.apiService.setMessage(data);
        });
    }

    public addNew() {
        const pie: ApiModel.PieDetail = { Id: 0, Symbol: '', Weight: 0, PieId: this.folioForm.controls.PieId.value, Enabled: true };
        this.PieDetails.push(pie);
    }

    public calcTotal(val: number) {
        this.Total = this.getTotal();
    }

    private getTotal() {
        return this.PieDetails.reduce((a, b) => Number(a) + Number((b['Weight'] || 0)), 0);
    }

    selectFolio(pie): void {
        this.folioForm.controls.PieId.setValue(pie.Id);
        this.pieId = pie.Id;

        this.loadPieSymbols();
       
    }

    loadPieSymbols(){
        if (this.pieId > 0){
        this.apiService.getPieDetail(this.pieId, 'q').subscribe((data) => {
            this.PieDetails = data;
            this.Total = this.getTotal();
        });
    }

    }

    // public calculateTotal() {
    //     return this.players.reduce((accum, curr) => accum + curr.goals, 0);
    //   }






    placeFolioOrder(): void {
        this.folioForm.controls.Broker.setValue(this.broker);
        console.log(this.folioForm.value);
        this.Globals.IsBusy = true;
        this.apiService.placeFolioOrder(this.folioForm.value).subscribe((data) => {
            this.apiService.setMessage2(data);
            this.dataSource = data.Object.Orders;
            this.Globals.IsBusy = false;
        });
    }

    onGridReady(params) {
        this.gridApi = params.api;
        this.gridApi.sizeColumnsToFit();
    }


}
