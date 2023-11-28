import { PostRequest } from "../request";

type RegisterType = {
  name: string;
  email: string;
  password: string;
};

const Register = async (data: RegisterType) => {
  let response = await PostRequest<any>("/Account/Register", data);

  return response.success;
};

export { Register };
