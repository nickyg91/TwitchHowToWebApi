import type { Claim } from './claim.model';

export interface ILoginResult {
  token: string;
  refreshToken: string;
  claims: Claim[];
}
