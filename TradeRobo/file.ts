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