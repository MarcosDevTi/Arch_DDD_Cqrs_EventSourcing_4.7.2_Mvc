"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var customer_1 = require("./customer");
function Welcome(person) {
    new customer_1.Customer().add();
    return "<h2>Hello " + person + ", Lets learn TypeScript</h2>";
}
function ClickMeButton() {
    var f = document.createElement("form");
    f.setAttribute('method', "post");
    f.setAttribute('action', "submit");
    var i = document.createElement("input"); //input element, text
    i.setAttribute('type', "text");
    i.setAttribute('name', "username");
    var s = document.createElement("input"); //input element, Submit button
    s.setAttribute('type', "submit");
    s.setAttribute('value', "Submit");
    f.appendChild(i);
    f.appendChild(s);
    document.getElementById('divMsg').appendChild(i);
    //let user: string = "MithunVP";
    // document.getElementById("divMsg").innerHTML = Welcome(user);
}
//# sourceMappingURL=app.js.map