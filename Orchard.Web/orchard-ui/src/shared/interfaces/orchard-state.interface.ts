import type { User } from '../models/user.model';

export interface IOrchardStore {
  token?: string | null;
  refreshToken?: string | null;
  user?: User | null;
}
