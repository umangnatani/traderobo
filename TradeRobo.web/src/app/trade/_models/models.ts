// tslint:disable-next-line: no-namespace
namespace ApiModel {

    export enum Environment {
        Prod = 'prod',
        Staging = 'staging',
        Test = 'test',
        Dev = 'dev',
        Local = 'local',
    }

    export interface Pie {
        Id: number;
        UserId: number;
        Name: string;
        Desc: string;
    }

    export interface Configuration {
        apiUrl: string;
        stage: Environment;
    }

    export interface ReturnType {
        Code: number;
        Success: boolean;
        Message: string;
        Object?: any;
    }

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


    export interface RHAuthResponse {
        ErrorMessage: string;
        MFARequired: boolean;
        isRHAuthenticated: boolean;
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

