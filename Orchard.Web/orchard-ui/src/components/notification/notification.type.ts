export type CreateNotification = {
  (options: {
    type?: string;
    title?: string;
    message?: string;
    autoClose?: boolean;
    duration?: number;
  }): void;
};
