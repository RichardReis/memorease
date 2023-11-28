import { PostRequest } from "../request";

export type EditDeckType = {
  id: number;
  name: string;
  userId: string;
  studyRoomId?: number;
};

const EditDeck = async (data: EditDeckType) => {
  let response = await PostRequest<any>("/StudyDeck/Edit", data);

  return response.success;
};

export { EditDeck };
