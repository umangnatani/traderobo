
import { ApiService } from 'app/trade/_services/api.service';



export abstract class BaseComponent {
    token: ApiModel.JwtToken;

    Globals = {IsBusy: false};

    constructor(protected apiService: ApiService) {
        this.token =  this.apiService.token;
        this.apiService.resetPageGlobals();
    }
}
