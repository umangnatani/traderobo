
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ApiService, NotificationService, BaseComponent, Globals } from 'app/my-app/_shared';


@Component({
    selector: 'app-td-order',
    templateUrl: './order.component.html',
    styleUrls: ['./order.component.scss'],
})
export class TDOrderComponent extends BaseComponent implements OnInit {

    broker: string

    constructor(
        private _formBuilder: FormBuilder,
        protected apiService: ApiService,
        private notificationService: NotificationService,
        private globals: Globals,
        private route: ActivatedRoute
    ) {
        super(apiService);

        this.route.paramMap.subscribe(params => {
            this.ngOnInit();
        });

    }

    ngOnInit(): void {

        this.apiService.setTile(`Place a $this.broker Trade`);
        this.broker = this.route.snapshot.paramMap.get('broker')
        // this.globals.PageTitle = 'Place a TD Trade';

     
    }

   }
