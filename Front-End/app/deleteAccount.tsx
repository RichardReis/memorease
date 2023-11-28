import React from "react";
import { Stack, useRouter } from "expo-router";
import Constants from "expo-constants";
import { Text, View, StyleSheet } from "react-native";
import Button from "../components/Button";
import Spacing from "../constants/Spacing";
import TextStyles from "../themedStyles/Text";
import Icon from "../components/Icon";
import Colors from "../constants/Colors";
import { DeleteUser } from "../service/Account/deleteUser";

const DeleteAccount: React.FC = () => {
  const router = useRouter();
  const RedirectTo = () => router.push("/");

  const deleteUser = async () => {
    let response = await DeleteUser();
    if (response) RedirectTo();
  };

  return (
    <View style={styles.container}>
      <Stack.Screen options={{ headerShown: false, headerLeft: () => null }} />

      <Icon color={Colors["light"].danger} name="hand-back-left" size={80} />
      <Text
        style={{
          ...TextStyles.title,
          textAlign: "center",
          color: Colors["light"].danger,
        }}
      >
        {"Pare!"}
      </Text>
      <Text style={{ ...TextStyles.labelBold, textAlign: "center" }}>
        {"Você está prestes a deletar sua conta de maneira permanente."}
      </Text>
      <Text style={{ ...TextStyles.labelBold, textAlign: "center" }}>
        {
          "Esta ação é irreversível e não será possivel recuperar a conta posteriormente."
        }
      </Text>
      <Text style={{ ...TextStyles.labelBold, textAlign: "center" }}>
        {"Ainda deseja prosseguir?"}
      </Text>
      <Button type="primary" title="Cancelar" onPress={() => router.back()} />
      <Button type="danger" title="Deletar Conta" onPress={deleteUser} />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    paddingTop: Constants.statusBarHeight,
    paddingHorizontal: Spacing.g,
    alignItems: "center",
    justifyContent: "center",
    gap: Spacing.m,
  },
});

export default DeleteAccount;
