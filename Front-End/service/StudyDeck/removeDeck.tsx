import { DeleteRequest } from "../request";

const RemoveDeck = async (studyDeckId: number) => {
  let response = await DeleteRequest<any>(
    `/StudyDeck/Remove?studyDeckId=${studyDeckId}`
  );

  return response.success;
};

export { RemoveDeck };
