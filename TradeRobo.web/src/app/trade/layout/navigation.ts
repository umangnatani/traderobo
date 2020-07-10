import { FuseNavigation } from '@fuse/types';

export const navigation: FuseNavigation[] = [
    {
        id       : 'robinhood',
        title    : 'Robinhood',
        translate: 'Robinhood',
        type     : 'group',
        icon     : 'apps',
        children : [
            {
                id       : 'Trade',
                title    : 'Trade',
                translate: 'Trade',
                type     : 'item',
                icon     : 'attach_money',
                url      : '/trade/robinhood'
            },
           
        ]
    },
    {
        id      : 'Admin',
        title   : 'Admin',
        type    : 'group',
        icon    : 'pages',
        children: [
            {
                id       : 'Manage Pie',
                title    : 'ManagePie',
                translate: 'Manage Pie',
                type     : 'item',
                icon     : 'attach_money',
                url      : '/tarde/robinhood'
            },
           
        ]
    },
    {
        id      : 'TD Ameritrade',
        title   : 'TD Ameritrade',
        type    : 'group',
        icon    : 'pages',
        children: [
            {
                id       : 'By Amout',
                title    : 'ByAmount',
                translate: 'By Amount',
                type     : 'item',
                icon     : 'attach_money',
                url      : '/tarde/robinhood'
            },
            {
                id       : 'Limit Order',
                title    : 'LimitOrder',
                translate: 'Limit Order',
                type     : 'item',
                icon     : 'lock',
                url      : '/tarde/robinhood'
            },
           
        ]
    },
];
