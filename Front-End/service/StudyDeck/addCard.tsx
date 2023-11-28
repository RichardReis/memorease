import { PostRequest } from "../request";

export type EditDeckType = {
  id: number;
  studyDeckId: number;
  front: string;
  back: string;
};

const AddCard = async (data: EditDeckType) => {
  let response = await PostRequest<any>("/StudyDeck/AddCards", data);

  return response.success;
};

export { AddCard };
