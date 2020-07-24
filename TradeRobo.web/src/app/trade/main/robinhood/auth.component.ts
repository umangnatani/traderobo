import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'app/trade/_shared';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { ApiService } from 'app/trade/_services/api.service';
import { ActivatedRoute, Router } from '@angular/router';

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
    });

    showMFA = false;


    constructor(
        private _formBuilder: FormBuilder,
        protected apiService: ApiService,
        private route: ActivatedRoute,
        private router: Router,
    ) {
        super(apiService);

    }

    ngOnInit(): void {
    }

    login(): void {
        console.log(this.loginForm.value);
        this.apiService.login(this.loginForm.value).subscribe((data) => {
            console.log(data);
            if (data.MFARequired) {
                // console.log('came in mfa');
                this.showMFA = true;
            }
            else if (data.isRHAuthenticated) {
                localStorage.setItem('isRHAuthenticated', 'true');
                this.apiService.setRHLoggedIn();
                // console.log(this.token);
            }
            else if (data.ErrorMessage) {
                this.apiService.setError(data.ErrorMessage);
            }

            
        });
    }

}
