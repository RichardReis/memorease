import { GetRequest } from "../request";

type UserInfoData = {
  id: string;
  firstName: string;
  name: string;
  email: string;
};

const UserInfo = async (): Promise<UserInfoData | null> => {
  let response = await GetRequest<UserInfoData>("/Account/UserInfo");

  if (response.success) return response.object;

  return null;
};

export { UserInfo };
