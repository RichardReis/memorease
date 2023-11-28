export type APIResponse<T> = {
  success: boolean;
  number?: number;
  object: T;
  message?: string;
};
