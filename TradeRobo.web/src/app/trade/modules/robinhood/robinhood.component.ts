import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ApiService } from '../../shared/services/api.service';
import { Subject } from 'rxjs';
import { BaseComponent } from 'app/trade/shared/component/base.component';





@Component({
    selector: 'robinhood',
    templateUrl: './robinhood.component.html',
    styleUrls: ['./robinhood.component.scss']
})



export class RobinhoodComponent extends BaseComponent implements OnInit, OnDestroy {
    

    constructor(
        private _formBuilder: FormBuilder,
        protected apiService: ApiService
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
