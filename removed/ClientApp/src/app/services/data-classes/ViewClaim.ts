import { IViewClaim } from "../interfaces/IViewClaim";

export class ViewClaim implements IViewClaim {
  referenceNumber: string = "";
  claimAmount: number = 0;
  receiptNumber: string = "";
  receiptAmount: number = 0;
  receiptDate: string = "";
  dateSubmitted: string = "";
  status: string = "";
  totalClaimAmount: number = 0;
}
