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

    // columnDefs = [
    //     {field: 'Symbol' },
    //     {field: 'Weight' }
    // ];

    private gridApi;


    columnDefs = [
        { field: "symbol" },

        {
            field: "quantity"
        },
        {
            field: "average_buy_price",
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
    this.apiService.setTile('Robinhood Positions');

    this.apiService.getPositions().subscribe((data) => {
        // console.log(data);
        this.dataSource = data;
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
