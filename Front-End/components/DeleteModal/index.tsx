import React, { useState } from "react";
import { Text, TouchableOpacity, View } from "react-native";
import Modal from "react-native-modal";
import Colors from "../../constants/Colors";
import Spacing from "../../constants/Spacing";
import TextStyles from "../../themedStyles/Text";
import Icon from "../Icon";
import Button from "../Button";
import { useRouter } from "expo-router";

interface DeleteModalProps {
  title: string;
  text?: string;
  returnHref: string;
  onConfirm?: () => void;
}

const DeleteModal: React.FC<DeleteModalProps> = ({
  title,
  returnHref,
  text,
  onConfirm,
}) => {
  const router = useRouter();
  const [openModal, setOpenModal] = useState<boolean>(false);

  const ToggleModal = () => setOpenModal(!openModal);

  const Confirm = () => {
    if (onConfirm) onConfirm();
    ToggleModal();
  };

  return (
    <>
      <TouchableOpacity onPress={ToggleModal}>
        <Icon color="red" name="trash-can-outline" />
      </TouchableOpacity>
      <Modal isVisible={openModal} onBackdropPress={ToggleModal}>
        <View
          style={{
            backgroundColor: Colors["light"].danger,
            borderRadius: Spacing.g,
          }}
        >
          <Text
            style={{
              ...TextStyles.labelBold,
              fontSize: 24,
              color: Colors["light"].white,
              padding: Spacing.g,
            }}
          >
            {title}
          </Text>
          <View
            style={{
              backgroundColor: Colors["light"].contentBackground,
              borderRadius: Spacing.g,
              padding: Spacing.g,
            }}
          >
            {text && (
              <Text style={{ ...TextStyles.label, fontSize: 20 }}>
                Você está prestes a deletar:{" "}
                <Text style={{ ...TextStyles.labelBold, fontSize: 24 }}>
                  {text}
                </Text>
              </Text>
            )}
            <Text style={{ ...TextStyles.label, fontSize: 20 }}>
              Continuar?
            </Text>
            <View
              style={{
                marginTop: Spacing.m,
                flexDirection: "row",
                justifyContent: "space-between",
              }}
            >
              <View style={{ width: "45%" }}>
                <Button title="Sim" type="danger" onPress={Confirm} />
              </View>
              <View style={{ width: "45%" }}>
                <Button title="Não" type="primary" onPress={ToggleModal} />
              </View>
            </View>
          </View>
        </View>
      </Modal>
    </>
  );
};

export default DeleteModal;
