import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'app/trade/shared/component/base.component';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ApiService } from 'app/trade/shared/services/api.service';

@Component({
    selector: 'app-order',
    templateUrl: './order.component.html',
    styleUrls: ['./order.component.scss'],
})
export class OrderComponent extends BaseComponent implements OnInit {

    orderForm: FormGroup;

    constructor(
        private _formBuilder: FormBuilder,
        protected apiService: ApiService
    ) {
        super(apiService);

    }

    ngOnInit(): void {
        this.orderForm = this._formBuilder.group({
            Symbol: ['', Validators.required],
            Quantity: [10, Validators.required],
            Price: [null],
        });
    }

    order(): void {
        this.Globals.IsBusy = true;
        this.apiService.placeOrder(this.orderForm.value).subscribe((data) => {
            console.log(data);
            this.apiService.showSuccess(data['Result']);
            this.Globals.IsBusy = false;
        },
        (err) => {
            console.log(err);
            this.Globals.IsBusy = false;
        });
    }

}
