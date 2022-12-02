import { Component, OnDestroy, OnInit } from "@angular/core";
import { Subscription } from "rxjs";
import { FSARule } from "../services/data-classes/FSARule";
import { EmployeeService } from "../services/employee-service";
import { FSARuleService } from "../services/fsa-rule.service";
import { IEmployeeOptionItem } from "../services/interfaces/IEmployeeOptionItem";

@Component({
  templateUrl: 'fsa-rule.component.html',
  selector: 'app-fsa-rule-component'
})
export class FSARuleComponent implements OnInit, OnDestroy {

  public fsaRule: FSARule;
  public employees: any[];

  private subscription: Subscription = new Subscription();


  constructor(private fsaRuleService: FSARuleService, private employeeService: EmployeeService) {
    this.fsaRule = new FSARule();
    this.employees = [];
    this.fsaRule.yearCoverage = new Date().getFullYear();

  }

  getEmployees(): void {
    //this.subscription.unsubscribe();
    this.subscription = this.employeeService.getEmployees().subscribe(
      result => {
        this.employees = result as IEmployeeOptionItem[];
        if (this.employees.length > 0) { this.fsaRule.employeeID = this.employees[0].id; }
      },
      errorResult => { alert("Failed to fetch employees."); }
    );
  }

  onSubmit(): void {
    this.validateClaimAmount();

    if (!this.validClaimAmount) {
      alert("Please check you amount input.");
      return;
    }

    this.subscription = this.fsaRuleService.addEmployeeFSARule(this.fsaRule).subscribe(
      result => { alert("Successfully created FSA"); },
      errorResult => { alert(errorResult.error.message); }
    );
  }

  ngOnInit(): void {
    this.getEmployees();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  onSelectEmployee(value: string): void {
    this.fsaRule.employeeID = parseInt(value);
    console.log(value);
  }


  onNumberChange(e: KeyboardEvent): void {
    if (e.key == "e") e.preventDefault();
    this.validateClaimAmount();
  }

  validateClaimAmount(): void {
    //if (e != null && e.key == "e") e.preventDefault();
    this.validClaimAmount = this.fsaRule.fSAAmount > 0;
  }

  public validClaimAmount: boolean = true;

}
