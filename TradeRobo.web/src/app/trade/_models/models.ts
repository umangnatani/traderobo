// tslint:disable-next-line: no-namespace
namespace ApiModel {

    export interface FavStocks {
        Id: number;
        Symbol: string;
    }

    export class User {
        Id: number;
        UserName: string;
        Role: string;
        Token?: string;
    }


    export interface JwtToken {
        userName: string;
        accessToken: string;
        isAuthenticated: boolean;
    }

    export interface Order {
        Symbol: string;
        Side: string;
        Type: string;
        TimeInForce: string;
        ExtendedHours: boolean;
        Quantity: number;
        Price: number;
        Amount: number;
        ResultCode: number;
        Result: string;
    }

    export interface PieDetail {
        Id: number;
        PieId: number;
        Symbol: string;
        Weight: number;
        Enabled: boolean;
    }

}

