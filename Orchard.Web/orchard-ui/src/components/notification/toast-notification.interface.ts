export interface ToastNotification {
  id?: string | null;
  title: string;
  type: 'info' | 'warning' | 'success' | 'danger' | 'link' | 'primary';
  duration: number;
  message: string;
  autoClose: boolean;
  isLight: boolean;
}
