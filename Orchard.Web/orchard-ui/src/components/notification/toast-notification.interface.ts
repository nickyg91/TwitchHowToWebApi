export class ToastNotification {
  id!: string;
  type!: 'info' | 'warning' | 'success' | 'danger' | 'link' | 'primary';
  duration = 5;
  closable = true;
  onClose?: Function;
}
