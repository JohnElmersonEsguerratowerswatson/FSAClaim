import { Component, OnDestroy, OnInit } from "@angular/core";
import { Subscription } from "rxjs";
import { FSARule } from "../services/data-classes/FSARule";
import { FSARuleService } from "../services/fsa-rule.service";

@Component({
  templateUrl: 'fsa-rule.component.html',
  selector: 'app-fsa-rule-component'
})
export class FSARuleComponent implements OnInit, OnDestroy {

  public fsaRule: FSARule;
  private subscription: Subscription = new Subscription();
  constructor(private fsaRuleService: FSARuleService) {
    this.fsaRule = new FSARule();
    //this.fsaRule.employeeID = 1;
    //this.fsaRule.employeeName = "John Doe";
    //this.fsaRule.fSAAmount = 5000;
    //this.fsaRule.yearCoverage = 2022;
  }

  onSubmit(): void {
    this.subscription = this.fsaRuleService.addEmployeeFSARule(this.fsaRule).subscribe(
      result => { alert("Successfully created FSA"); },
      errorResult => { }
    );
  }

  ngOnInit(): void { }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
