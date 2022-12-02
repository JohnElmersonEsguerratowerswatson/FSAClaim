import { Component, ElementRef, OnInit, Renderer2, ViewChild } from "@angular/core";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { TransactClaim } from "../services/data-classes/TransactClaim";
import { ClaimConstructService } from "../services/claim-construct.service";
import { ITransactClaim } from "../services/interfaces/ITransactClaim";
import { ITransactClaimResult } from "../services/interfaces/ITransactClaimResult";
import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";


@Component({
  selector: 'app-claim-const-component',
  templateUrl: 'claim-construct.component.html'
})
export class ClaimConstructComponent implements OnInit {

  public claim: TransactClaim;
  public subscription: Subscription;
  public claimResult: ITransactClaimResult;

  @ViewChild("receiptAmountField")
  receiptAmount!: ElementRef;

  constructor(private renderer: Renderer2, private router: Router, private claimConstructService: ClaimConstructService) {
    this.claimResult = {} as ITransactClaimResult;
    this.subscription = {} as Subscription;

    this.claim = this.router.getCurrentNavigation()?.extras.state as ITransactClaim;
    if (this.claim == null) this.claim = new TransactClaim();
  }


  onSubmit(e: Event): void {
    if (!this.validateModel()) {
      alert("Please check error messages in your input.");
      e.preventDefault();
      return;
    }

    let isNew: boolean = this.claim.referenceNumber.length <= 0;
    this.subscription = this.claimConstructService.claimConstructAPI(isNew, this.claim).subscribe(
      result => {
        console.log(JSON.stringify(result));
        this.claimResult = result as ITransactClaimResult;
        alert("Successfull submitted claim for approval.");
        this.router.navigateByUrl("claim-list");
      },
      err => { alert(err.error.message); }
    );

  }


  ngOnInit(): void {

    //alert(this.receiptAmount);
    //this.receiptAmount?.nativeElement.focus();
    //let response = {
    //  "headers": {
    //    "normalizedNames": {},
    //    "lazyUpdate": null
    //  },
    //  "status": 400,
    //  "statusText": "OK",
    //  "url": "https://localhost:7254/api/Claims/Create",
    //  "ok": false,
    //  "name": "HttpErrorResponse",
    //  "message": "Http failure response for https://localhost:7254/api/Claims/Create: 400 OK",
    //  "error": { "message": "You only have 0.00.", "isSuccess": false }
    //}
  }


  requiredFieldValidator(value: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const required = value != null && value.length > 0;
      return required ? { required: { value: control.value } } : null;
    };
  }


  numericFieldValidator(value: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const numeric = value != null && parseFloat(value) != NaN;
      return numeric ? { numeric: { value: control.value } } : null;
    };
  }

  dateFieldValidator(value: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const date = value != null && Date.parse(value) != NaN;
      return date ? { date: { value: control.value } } : null;
    };
  }

  onNumberChange(e: KeyboardEvent): void {
    //this.validateModel();
    if (e.key == "e") e.preventDefault();
  }

  validateModel(): boolean {
    this.validClaim = this.claim.claimAmount > 0;
    this.validReceiptAmount = this.claim.receiptAmount > 0;
    this.validReceiptNumber = this.claim.receiptNumber.length > 0;
    this.validReceiptDate = this.claim.receiptDate != "" && Date.parse(this.claim.receiptDate) != NaN;
    return this.validClaim && this.validReceiptAmount && this.validReceiptNumber && this.validReceiptDate;
  }

  validateClaim(e: KeyboardEvent): void{
    this.validClaim = this.claim.claimAmount > 0;
    if (e.key == "e") e.preventDefault();
   // return this.validClaim;
  }
  validateReceiptAmount(e: KeyboardEvent): void {
    this.validReceiptAmount = this.claim.receiptAmount > 0;
    if (e.key == "e") e.preventDefault();
    //return this.validReceiptAmount;
  }
  validateReceiptNumber(e: KeyboardEvent): void {
    this.validReceiptNumber = this.claim.receiptNumber.length > 0;
    //if (e.key == "e") e.preventDefault();
   // return this.validReceiptNumber;
  }
  validateReceiptDate(e: KeyboardEvent): void {
    this.validReceiptDate = this.claim.receiptDate != "" && Date.parse(this.claim.receiptDate) != NaN;
    //if (e.key == "e") e.preventDefault();
    //return this.validReceiptDate;
  }

  onRequiredFill(flag: boolean) {
    flag = true;
  }

  onReceiptEnter() {
    this.validClaim = true;
  }

  public validClaim: boolean = true;
  public validReceiptAmount: boolean = true;
  public validReceiptNumber: boolean = true;
  public validReceiptDate: boolean = true;

}
