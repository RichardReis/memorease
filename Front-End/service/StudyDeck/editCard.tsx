import { PostRequest } from "../request";

type EditDeckType = {
  id: number;
  studyDeckId: number;
  front: string;
  back: string;
};

const EditCard = async (data: EditDeckType) => {
  let response = await PostRequest<any>("/StudyDeck/EditCard", data);

  return response.success;
};

export { EditCard };
