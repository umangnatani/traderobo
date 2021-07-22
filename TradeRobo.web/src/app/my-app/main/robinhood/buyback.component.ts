import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { ApiService, NotificationService, BaseComponent, Globals } from 'app/my-app/_shared';

@Component({
  selector: 'app-buyback',
  templateUrl: './buyback.component.html',
  styleUrls: ['./buyback.component.scss']
})
export class BuybackComponent extends BaseComponent implements OnInit, OnDestroy {
    
    dataSource;

    defaultColDef;

    rowClassRules;

    rowSelection;

    private gridApi;


    columnDefs = [
        { field: "Symbol" },

        {
            field: "Quantity"
        },
        {
            field: "Price",
        },
        {
            field: "TimeStamp",
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
            // flex: 1,
            minWidth: 150,
            sortable: true,
        };

        this.rowSelection = 'multiple';

    }

  ngOnInit(): void {
    this.apiService.setTile('Robinhood Positions');

    this.apiService.getBuyBackPositions().subscribe((data) => {
        // console.log(data);
        this.dataSource = data;
    });
  }

  placeOrder(): void{
    const payload = this.gridApi.getSelectedRows();
    this.apiService.buyBack(payload).subscribe((data) => {
        console.log(data);
        // this.dataSource = data;
    });
  }

  onGridReady(params) {
    this.gridApi = params.api;
    this.gridApi.sizeColumnsToFit();
}

  ngOnDestroy(): void {
      
}

}
