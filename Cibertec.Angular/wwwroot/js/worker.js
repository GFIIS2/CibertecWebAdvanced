var self = this;

self.onmessage = function (message) {
    //debugger;
    var splitedLine = message.data.split(',');
    var member = {
        member_no: splitedLine[0],
        lastname: splitedLine[1],
        firstname: splitedLine[2],
        middleinitial: splitedLine[3],
        street: splitedLine[4],
        city: splitedLine[5],
        state_prov: splitedLine[6],
        country: splitedLine[7],
        mail_code: splitedLine[8],
        phone_no: splitedLine[9],
        issue_dt: splitedLine[10],
        expr_dt: splitedLine[11],
        corp_no: splitedLine[12],
        prev_balance: splitedLine[13],
        curr_balance: splitedLine[14],        
        member_code: splitedLine[15]
    }

    self.postMessage(member);
}