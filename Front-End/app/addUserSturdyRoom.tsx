import React from "react";

import { BuildInputsType } from "../components/Form/BuildInputs";
import FormScreenStructure from "../components/FormScreenStructure";
import { useLocalSearchParams, useRouter } from "expo-router";
import { AddUser } from "../service/Room/addUser";

const CreateDeck: React.FC = () => {
  const router = useRouter();
  const params = useLocalSearchParams();
  const { roomId } = params;

  const inputs: BuildInputsType = [
    {
      name: "email",
      label: "E-mail",
      required: true,
      type: "text",
    },
  ];

  const create = async (data: any) => {
    let response = await AddUser({
      ...data,
      roomId: roomId,
    });
    if (response) router.push("/roomUsers");
  };

  return (
    <FormScreenStructure
      title="Vincular um Usuário"
      inputs={inputs}
      buttonText="Vincular"
      onSubmit={create}
    />
  );
};

export default CreateDeck;
