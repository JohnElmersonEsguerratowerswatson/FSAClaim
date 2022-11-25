import { IFSARule } from "../services/interfaces/IFSARule";

export class FSARule implements IFSARule {
  fSAAmount: number = 0;
  yearCoverage: number = 0;
  employeeID: number = 0;
  employeeName: string = "";
}