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

    orderForm: FormGroup;
    orderSide = 'buy';
    showAdvanced = false;


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

        this.orderForm = this._formBuilder.group({
            Symbol: ['', Validators.required],
            Quantity: [10, Validators.required],
            Price: [null],
            Side: ['buy'],
            Increment: [.20],
            Total: [5],
        });

        this.notificationService.currentSymbol.subscribe(val => this.orderForm.controls.Symbol.setValue(val));
    }

    checkAdvanced(){
        this.showAdvanced = !this.showAdvanced;
    }

    selectOrderSide(val: string): void {
        this.orderForm.controls.Side.setValue(val);
        this.orderSide = val;
    }

    order(): void {
        this.Globals.IsBusy = true;
        this.apiService.placeTDOrder(this.orderForm.value).subscribe((data) => {
            console.log(data);
            this.apiService.setMessage('Orders placed successfully');
            this.Globals.IsBusy = false;
        });
    }

}
