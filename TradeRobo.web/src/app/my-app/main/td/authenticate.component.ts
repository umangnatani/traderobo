import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, FormControl } from "@angular/forms";
import {
    ApiService,
    NotificationService,
    BaseComponent,
    Globals,
} from "app/my-app/_shared";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    selector: "app-authenticate",
    templateUrl: "./authenticate.component.html",
    styleUrls: ["./authenticate.component.scss"],
})
export class AuthenticateComponent extends BaseComponent implements OnInit {
    loginForm = new FormGroup({
        code: new FormControl(""),
    });

    showMFA = false;
    authUrl = '';

    constructor(
        private _formBuilder: FormBuilder,
        public apiService: ApiService,
        private route: ActivatedRoute,
        private router: Router
    ) {
        super(apiService);
    }

    ngOnInit(): void {
        this.apiService.getAuthUrl().subscribe((data) => {
            console.log(data);
            this.authUrl = data['response'];
        });

    }

    clearToken(){
        this.apiService.RHLogOut();
      }

    

    login(): void {
        //console.log(this.loginForm.value);
        this.apiService.TDlogin(this.loginForm.value).subscribe((data) => {
            this.apiService.setMessage(data);
            //this.dataSource = data.Object.Orders;
            this.Globals.IsBusy = false;
        });
    }
}
