import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'app/trade/_shared';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { ApiService } from 'app/trade/_services/api.service';

@Component({
    selector: 'app-login',
    templateUrl: './auth.component.html',
    styleUrls: ['./auth.component.scss']
})
export class AuthComponent extends BaseComponent implements OnInit {

    loginForm = new FormGroup({
        userName: new FormControl(''),
        passWord: new FormControl(''),
        mfaToken: new FormControl(''),
        deviceToken: new FormControl(''),
    });

    showMFA = false;


    constructor(
        private _formBuilder: FormBuilder,
        protected apiService: ApiService
    ) {
        super(apiService);

    }

    ngOnInit(): void {
    }

    login(): void {
        console.log(this.loginForm.value);
        this.apiService.login(this.loginForm.value).subscribe((data) => {
            // console.log(data);
            if (data['isAuthenticated'] == false) {
                console.log('came in mfa');
                this.showMFA = true;
                this.loginForm.controls.deviceToken.setValue(data['deviceToken']);
            }
            else {
                localStorage.setItem('token', JSON.stringify(data));
                this.token = JSON.parse(localStorage.getItem('token'));
                console.log(this.token);
            }
            // console.log(this.loginForm.controls.deviceToken.value);
        });
    }

}
