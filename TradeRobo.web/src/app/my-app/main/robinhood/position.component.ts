import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { ApiService, NotificationService, BaseComponent, Globals } from 'app/my-app/_shared';



@Component({
  selector: 'app-position',
  templateUrl: './position.component.html',
  styleUrls: ['./position.component.scss']
})
export class PositionComponent extends BaseComponent implements OnInit, OnDestroy {
    
    dataSource;

    defaultColDef;

    rowClassRules;

    rowSelection;

    cash;
    portfolio_value;
    buying_power;

    // columnDefs = [
    //     {field: 'Symbol' },
    //     {field: 'Weight' }
    // ];

    private gridApi;


    columnDefs = [
        { field: "symbol",
        cellClass: "text-bold", },

       
       
        {
            field: "current_price",
            headerName: "Last Price",
            cellClass: "text-bold",
            cellClassRules: {
                "red-900-fg": "data.isRed",
                "green-900-fg": "!data.isRed",
              },
            valueFormatter: params => params.value.toFixed(2),
        },
        {
            field: "pct_change",
            headerName: "% Change",
            cellStyle: { "font-weight": "bold" },
            cellClassRules: {
                "red-900-fg": "data.isRed",
                "green-900-fg": "!data.isRed",
              },
            valueFormatter: (params) => {
                return params.value.toFixed(2) + "%";
            },
        },
        {
            field: "market_value",
            headerName: "Equity",
            cellClass: "text-bold blue-900-fg",
            valueFormatter: params => params.value.toFixed(2),
        },
        {
            field: "avg_entry_price",
            headerName: "Entry Price",
            cellClass: "text-bold blue-fg",
            valueFormatter: params => params.value.toFixed(2),
        },
        {
            field: "qty",
            headerName: "Quantity",
            cellClass: "text-bold",
            valueFormatter: params => params.value.toFixed(2),
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
            flex: 1,
            minWidth: 100,
            sortable: true,
        };

        this.rowSelection = 'multiple';

    }

  ngOnInit(): void {

    this.apiService.getTradingAccount().subscribe((data) => {
        // console.log(data);
        this.dataSource = data['positions'];
        this.cash = data['cash'];
        this.portfolio_value = data['portfolio_value'];
        this.buying_power = data['buying_power'];
    });
  }

  placeOrder(): void{
    const data = this.gridApi.getSelectedRows();
    console.log(data);
  }

  onGridReady(params) {
    this.gridApi = params.api;
    this.gridApi.sizeColumnsToFit();
}

  ngOnDestroy(): void {
      
}

}
