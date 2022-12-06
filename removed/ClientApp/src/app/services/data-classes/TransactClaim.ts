import { ITransactClaim } from "../interfaces/ITransactClaim";

export class TransactClaim implements ITransactClaim {
  referenceNumber: string = "";
  claimAmount: number = 0;
  receiptNumber: string = "";
  receiptAmount: number = 0;
  receiptDate: string = "";
}
