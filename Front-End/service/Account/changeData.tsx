import { PostRequest } from "../request";

type ChangeDataType = {
  name: string;
  email: string;
};

const ChangeData = async (data: ChangeDataType) => {
  let response = await PostRequest<any>("/Account/ChangeData", data);

  return response.success;
};

export { ChangeData };
