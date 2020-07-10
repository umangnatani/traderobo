
import { ApiService } from '../../shared/services/api.service';



export abstract class BaseComponent {
    token: ApiModel.JwtToken;

    Globals = {IsBusy: false};

    constructor(protected apiService: ApiService) {
        this.token =  this.apiService.token;
    }
}
