import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { ApiService, NotificationService, BaseComponent, Globals } from 'app/my-app/_shared';
import { ActivatedRoute } from '@angular/router';




@Component({
    selector: 'robinhood',
    templateUrl: './robinhood.component.html',
    styleUrls: ['./robinhood.component.scss']
})



export class RobinhoodComponent extends BaseComponent implements OnInit, OnDestroy {

    broker: string;
    

    constructor(
        private _formBuilder: FormBuilder,
        public apiService: ApiService,
        private notificationService: NotificationService,
        private globals: Globals,
        private route: ActivatedRoute
    ) {
        super(apiService);

        this.route.paramMap.subscribe(params => {
            this.ngOnInit();
        });

    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        this.broker = this.route.snapshot.paramMap.get('broker')
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
