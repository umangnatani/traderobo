import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'app/trade/_shared';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiService } from 'app/trade/_services/api.service';

@Component({
    selector: 'app-folio',
    templateUrl: './folio.component.html',
    styleUrls: ['./folio.component.scss']
})
export class FolioComponent extends BaseComponent implements OnInit {

    pieValue = '';
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
        { field: "Symbol" },

        {
            field: "Weight"
        },
        {
            field: "Quote.last_trade_price",
            headerName: "Last Price"
        },
        {
            field: "Quote.pct_change",
            headerName: "% Change",
            valueFormatter: (params) => {
                return params.value + '%';
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
            'red-900': 'data.Quote.pct_change < 0',
            'green-900': 'data.Quote.pct_change > 0',
        };

    }


    ngOnInit(): void {
        this.folioForm = this._formBuilder.group({
            PieId: [],
            Amount: [100, Validators.required],
            Side: ['buy'],
            PriceWeighted: [false]
        });


        // console.log(this.token.isAuthenticated);

        this.apiService.getPies().subscribe((data) => {
            this.Pies = data;
        });

    }

    selectOrderSide(val: string): void {
        this.folioForm.controls.Side.setValue(val);
        this.orderSide = val;
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
        this.pieValue = pie.Name;

        this.apiService.getPieDetail(pie.Id, 'q').subscribe((data) => {
            this.PieDetails = data;
            this.Total = this.getTotal();
        });
    }

    // public calculateTotal() {
    //     return this.players.reduce((accum, curr) => accum + curr.goals, 0);
    //   }






    placeFolioOrder(): void {
        // console.log(this.folioForm.value);
        this.Globals.IsBusy = true;
        this.apiService.placeFolioOrder(this.folioForm.value).subscribe((data) => {
            this.apiService.setMessage2(data);
            this.dataSource = data.Object;
            this.Globals.IsBusy = false;
        });
    }

    onGridReady(params) {
        this.gridApi = params.api;
        this.gridApi.sizeColumnsToFit();
    }


}
