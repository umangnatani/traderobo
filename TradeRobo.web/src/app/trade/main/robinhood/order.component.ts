import { Component, OnInit, Input } from '@angular/core';
import { BaseComponent } from 'app/trade/_shared';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ApiService, NotificationService } from 'app/trade/_services';
import { Globals } from 'app/trade/_helpers';

@Component({
    selector: 'app-order',
    templateUrl: './order.component.html',
    styleUrls: ['./order.component.scss'],
})
export class OrderComponent extends BaseComponent implements OnInit {

    form: FormGroup;
    orderSide = 'buy';
    showAdvanced = false;
    @Input() OrderTo = 'RH';


    constructor(
        private _formBuilder: FormBuilder,
        protected apiService: ApiService,
        private notificationService: NotificationService,
        private globals: Globals
    ) {
        super(apiService);

    }

    ngOnInit(): void {

        this.form = this._formBuilder.group({
            Symbol: ['', Validators.required],
            Quantity: [10, Validators.required],
            Price: [null],
            Side: ['buy'],
            Increment: [.20],
            Total: [5],
        });

        this.notificationService.currentSymbol.subscribe(val => this.f.Symbol.setValue(val));
    }

    // convenience getter for easy access to form fields
    get f() { return this.form.controls; }

    checkAdvanced(){
        this.showAdvanced = !this.showAdvanced;
    }

    selectOrderSide(val: string): void {
        this.form.controls.Side.setValue(val);
        this.orderSide = val;
    }

    order(): void {
        console.log(this.form.value);
        if (!this.showAdvanced){
            this.f.Total.setValue(1);
        }
        this.Globals.IsBusy = true;
        this.apiService.placeOrder(this.form.value, this.OrderTo).subscribe((data) => {
            // console.log(data);
            this.apiService.setMessage2(data);
            this.Globals.IsBusy = false;
        });
    }

}
