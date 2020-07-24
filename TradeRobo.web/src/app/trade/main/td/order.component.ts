import { NotificationService } from 'app/trade/_services/notification.service';
import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'app/trade/_shared';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ApiService } from 'app/trade/_services/api.service';
import { Globals } from 'app/trade/_helpers';

@Component({
    selector: 'app-td-order',
    templateUrl: './order.component.html',
    styleUrls: ['./order.component.scss'],
})
export class TDOrderComponent extends BaseComponent implements OnInit {



    constructor(
        private _formBuilder: FormBuilder,
        protected apiService: ApiService,
        private notificationService: NotificationService,
        private globals: Globals
    ) {
        super(apiService);

    }

    ngOnInit(): void {

        this.apiService.setTile('Place a TD Trade');
        // this.globals.PageTitle = 'Place a TD Trade';

     
    }

   }
