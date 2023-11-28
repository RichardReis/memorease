import { PostRequest } from "../request";

type SaveStudyType = {
  id: number;
  answer: number;
};

const SaveStudy = async (data: SaveStudyType) => {
  let response = await PostRequest<any>(
    `/StudyDeck/SaveStudy?id=${data.id}&answer=${data.answer}`,
    {}
  );

  return response.success;
};

export { SaveStudy };
