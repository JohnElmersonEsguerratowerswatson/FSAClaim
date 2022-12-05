import { IGetClaimResult } from "../interfaces/IGetClaimsResult";
import { IViewClaim } from "../interfaces/IViewClaim";

export class GetClaimsResult implements IGetClaimResult {
  claims: IViewClaim[] = [];
  fsaAmount: number = 0;
  yearCoverage: number = 0;
  employeeID: number = 0;
  employeeName: string = "";
  availableFSA: number = 0;
  pendingClaims: number = 0;
  approvedClaims: number = 0;

}
