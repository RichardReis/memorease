import { GetRequest } from "../request";

export type LoadStudyData = {
  id: number;
  studyDeckId: number;
  studyDeckName: string;
  front: string;
  back: string;
};

const LoadStudy = async (deckid: number): Promise<LoadStudyData | null> => {
  let response = await GetRequest<LoadStudyData>(
    `/StudyDeck/LoadStudy?deckid=${deckid}`
  );

  if (response.success) return response.object;

  return null;
};

export { LoadStudy };
