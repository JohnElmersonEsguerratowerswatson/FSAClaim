import { Component } from "@angular/core";
import { FSARule } from "../data-classes/FSARule";

@Component({
  templateUrl: 'fsa-rule.component.html',
  selector: 'app-fsa-rule-component'
})
export class FSARuleComponent {

  public fsaRule: FSARule;

  constructor() {
    this.fsaRule = new FSARule();
    this.fsaRule.employeeID = 1;
    this.fsaRule.employeeName = "John Doe";
    this.fsaRule.fSAAmount = 5000;
    this.fsaRule.yearCoverage = 2022;
  }

  onSubmit():void {
    alert("test");
  }
}
