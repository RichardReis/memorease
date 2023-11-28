import { PostRequest } from "../request";

type CreateDeckType = {
  id: number;
  name: string;
  userId: string;
  studyRoomId?: number;
};

const CreateDeck = async (data: CreateDeckType) => {
  let response = await PostRequest<any>("/StudyDeck/Create", data);

  return response.success;
};

export { CreateDeck };
