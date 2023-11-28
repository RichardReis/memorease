import { DeleteRequest } from "../request";

const RemoveCard = async (studyCardId: number) => {
  let response = await DeleteRequest<any>(
    `/StudyDeck/RemoveCard?studyCardId=${studyCardId}`
  );

  return response.success;
};

export { RemoveCard };
