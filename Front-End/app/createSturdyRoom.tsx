import React from "react";

import { BuildInputsType } from "../components/Form/BuildInputs";
import FormScreenStructure from "../components/FormScreenStructure";
import { CreateRoom } from "../service/Room/createRoom";
import { useRouter } from "expo-router";

const CreateSturdyRoom: React.FC = () => {
  const router = useRouter();
  const inputs: BuildInputsType = [
    {
      name: "name",
      label: "Nome da Sala",
      required: true,
      type: "text",
    },
    // {
    //   name: "isPublic",
    //   label: "Sala Pública",
    //   required: true,
    //   type: "text",
    // },
  ];

  const create = async (data: any) => {
    let response = await CreateRoom(data);
    if (response) router.push("/studyRoomsList");
  };

  return (
    <FormScreenStructure
      title="Criar Nova Sala"
      inputs={inputs}
      buttonText="Salvar"
      onSubmit={create}
    />
  );
};

export default CreateSturdyRoom;
