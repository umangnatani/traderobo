import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { ApiService, NotificationService, BaseComponent, Globals } from 'app/my-app/_shared';




@Component({
    selector: 'robinhood',
    templateUrl: './robinhood.component.html',
    styleUrls: ['./robinhood.component.scss']
})



export class RobinhoodComponent extends BaseComponent implements OnInit, OnDestroy {
    

    constructor(
        private _formBuilder: FormBuilder,
        public apiService: ApiService,
        private notificationService: NotificationService,
        private globals: Globals
    ) {
        super(apiService);

    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        // Reactive Form
        // this.globals.PageTitle = 'Place a Robinhood Trade';
        this.apiService.setTile('Place a Robinhood Trade');
        // this.apiService.Error = 'An Error';

    }

    

    
    test(): void {
        this.apiService.test().subscribe((data) => {
            console.log(data);
            // this.dataSource = data;
        });
    }


    

    /**
     * On destroy
     */
    ngOnDestroy(): void {
      
    }


}
