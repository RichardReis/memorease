import { DeleteRequest, singOut } from "../request";

const DeleteUser = async () => {
  let response = await DeleteRequest<any>("/Account/DeleteUser");

  if (response.success) await singOut();

  return response.success;
};

export { DeleteUser };
