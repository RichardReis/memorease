import { signIn } from "../request";

type AuthUserType = {
  email: string;
  password: string;
};

const AuthUser = async ({ email, password }: AuthUserType) => {
  let response = await signIn({ email, password });

  return response.Success;
};

export { AuthUser };
