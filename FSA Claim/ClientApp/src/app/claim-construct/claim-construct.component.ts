import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { ITransactClaim } from "../claim-data-transfer-objects/ITransactClaim";

@Component({
  selector: 'app-claim-const-component',
  templateUrl: 'claim-construct.component.html'
})

export class ClaimConstructComponent implements OnInit{

  public claim: ITransactClaim;

  constructor(private router: Router) {
    this.claim = {} as ITransactClaim;
    this.claim = this.router.getCurrentNavigation()?.extras.state as ITransactClaim;
  }

  ngOnInit(): void {
   
    
  }


}
