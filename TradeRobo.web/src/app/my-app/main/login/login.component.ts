import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { BaseComponent } from 'app/my-app/_shared';
import { AuthenticationService } from 'app/my-app/_shared';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    returnUrl: string;
    isValid = true;

    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     * @param {FormBuilder} _formBuilder
     */
    constructor(
        private _fuseConfigService: FuseConfigService,
        private route: ActivatedRoute,
        private router: Router,
        private _formBuilder: FormBuilder,
        protected service: AuthenticationService
    ) {

        if (this.service.currentUserValue) { 
            this.router.navigate(['/']);
        }

        this._fuseConfigService.config = {
            layout: {
                navbar: {
                    hidden: true
                },
                toolbar: {
                    hidden: true
                },
                footer: {
                    hidden: true
                },
                sidepanel: {
                    hidden: true
                }
            }
        };

    }



    ngOnInit(): void {
        this.loginForm = this._formBuilder.group({
            UserName: ['', [Validators.required]],
            Password: ['', Validators.required]
        });

        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    // convenience getter for easy access to form fields
    get f() { return this.loginForm.controls; }


    authenticate(): void {
        this.service.authenticate(this.loginForm.value).subscribe((data) => {
            if (data){
                //this.router.navigate([this.returnUrl]);
                this.router.navigate(['/']);
            }
            else{
                this.isValid = false;
            }
        });
    }

}
